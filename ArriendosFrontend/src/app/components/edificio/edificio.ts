import { Component, inject, OnInit, signal } from '@angular/core';

import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { EdificioService } from '../../services/edificio';
import { EdificioReadDto, InmuebleReadDto } from '../../models/arriendos.models';
import { InmuebleService } from '../../services/inmueble';
@Component({
  imports: [ReactiveFormsModule],
  selector: 'app-edificio',
  standalone: true,
  styleUrl: './edificio.css',
  templateUrl: './edificio.html',
})
export class Edificio implements OnInit {
  private edificioService = inject(EdificioService);
  private fb = inject(FormBuilder);
  private inmuebleService = inject(InmuebleService);
  edificios = signal<EdificioReadDto[]>([]);
  cargando = signal<boolean>(true);
  errorMensaje = signal<string | null>(null);

  edificioEdicionId = signal<number | null>(null);
  inmuebles = signal<InmuebleReadDto[]>([]);
  mostrarInactivos = signal<boolean>(false);

  formEdificio: FormGroup = this.fb.group({
    nombre: ['', [Validators.required, Validators.maxLength(100)]],
    direccion: ['', [Validators.required, Validators.maxLength(200)]],
  });

  ngOnInit(): void {
    this.cargarEdificios();
  }
  cargarEdificios(): void {
    this.cargando.set(true);

    this.inmuebleService.getInmueble().subscribe({
      next: (data) => {
        this.inmuebles.set(data);
      },
      error: (err) => console.error('Error al obtener inmuebles:', err),
    });
    this.edificioService.getEdificios(this.mostrarInactivos()).subscribe({
      next: (data) => {
        this.edificios.set(data);
        this.cargando.set(false);
      },
      error: (err) => {
        this.errorMensaje.set('Error al conectar con la api');
        this.cargando.set(false);
        console.error(err);
      },
    });
  }

  toggleMostrarInactivos(): void {
    this.mostrarInactivos.update((v) => !v);
    this.cargarEdificios();
  }

  reactivar(id: number) {
    this.edificioService.cambiarEstadoEdificio(id, true).subscribe({
      next: () => {
        this.cargarEdificios();
      },
      error: (err) => {
        this.errorMensaje.set('Error al intentar reactivar');
        console.error('error al reactivar', err);
      },
    });
  }

  guardarEdificio(): void {
    if (this.formEdificio.invalid) return;

    this.edificioService.createEdificio(this.formEdificio.value).subscribe({
      next: (nuevoEdificio) => {
        this.edificios.update((lista) => [...lista, nuevoEdificio]);
        this.formEdificio.reset();
        this.errorMensaje.set(null);
      },
      error: (err) => {
        this.errorMensaje.set('No se pudo guardar el edificio');
        console.error(err);
      },
    });
  }

  seleccionarEditar(edificio: EdificioReadDto): void {
    this.edificioEdicionId.set(edificio.idEdificio);
    this.formEdificio.patchValue({ direccion: edificio.direccion, nombre: edificio.nombre });
  }

  cancelarEdicion(): void {
    this.formEdificio.reset();
    this.edificioEdicionId.set(null);
  }

  guardar(): void {
    if (this.formEdificio.invalid) return;
    const isEditado = this.edificioEdicionId();

    if (isEditado !== null) {
      const dto = this.formEdificio.value;
      this.edificioService.updateEdificio(isEditado, dto).subscribe({
        next: () => {
          this.cargarEdificios();
          this.cancelarEdicion();
        },
        error: (err) => {
          this.errorMensaje.set('ocurrio un problema al actualizar');
          console.error('Error al actualizar', err);
        },
      });
    } else {
      this.guardarEdificio();
    }
  }

  eliminar(id: number): void {
    const tieneInmuebles = this.inmuebles().some((inm) => inm.idEdificio === id);
    if (tieneInmuebles) {
      alert(
        '⚠️ No se puede eliminar este edificio porque tiene inmuebles/departamentos registrados. Elimina o reasigna los inmuebles primero.',
      );
      return;
    }
    if (confirm('¿Estás seguro de que deseas eliminar este inmueble?')) {
      this.edificioService.cambiarEstadoEdificio(id, false).subscribe({
        next: () => {
          this.cargarEdificios();
          if (this.edificioEdicionId() == id) {
            this.cancelarEdicion();
          }
        },
        error: (err) => {
          this.errorMensaje.set('Error al intentar eliminar');
          console.error('error al eliminar', err);
        },
      });
    }
  }
}
