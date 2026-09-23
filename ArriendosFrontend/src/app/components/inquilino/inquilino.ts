import { Component, inject, signal } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ReactiveFormsModule } from '@angular/forms';
import { OnInit } from '@angular/core';
import { InquilinoService } from '../../services/inquilino';
import { InquilinoReadDto } from '../../models/arriendos.models';

@Component({
  imports: [ReactiveFormsModule],
  standalone: true,
  selector: 'app-inquilino',
  styleUrl: './inquilino.css',
  templateUrl: './inquilino.html',
})
export class Inquilino implements OnInit {
  private inquilinoService = inject(InquilinoService);
  private fb = inject(FormBuilder);

  inquilinos = signal<InquilinoReadDto[]>([]);
  cargando = signal<boolean>(true);
  errorMensaje = signal<string | null>(null);
  idInquilinoActualizar = signal<number | null>(null);
  mostrarInactivos = signal<boolean>(false);

  formInquilino: FormGroup = this.fb.group({
    nombres: ['', [Validators.required, Validators.maxLength(250)]],
    identificacion: ['', [Validators.required, Validators.minLength(10), Validators.maxLength(10)]],
    telefono: ['', [Validators.required]],
    email: ['', [Validators.required, Validators.email]],
  });

  ngOnInit(): void {
    this.cargardatos();
  }

  cargardatos(): void {
    this.cargando.set(true);
    this.inquilinoService.getInquilinos(this.mostrarInactivos()).subscribe({
      next: (data) => {
        this.inquilinos.set(data);
        this.cargando.set(false);
      },
      error: (err) => {
        this.errorMensaje.set('Error al caragr los datos');
        console.error('error:', err);
      },
    });
  }

  toggleMostrarInactivos(): void {
    this.mostrarInactivos.update((v) => !v);
    this.cargardatos();
  }

  reactivar(id: number): void {
    
    this.inquilinoService.cambiarEstadoInquilino(id,true).subscribe({
      next: () => {
        console.log("tartando de activar");
        this.cargardatos();
      },
      error: (err) => {
        this.errorMensaje.set('Error al intentar reactivar');
        console.error('error al reactivar', err);
      },
    });
  }
  crear(): void {
    if (this.formInquilino.invalid) {
      return;
    }
    this.inquilinoService.createInquilino(this.formInquilino.value).subscribe({
      next: (inquilino) => {
        this.inquilinos.update((lista) => [...lista, inquilino]);
        this.formInquilino.reset();
        this.errorMensaje.set(null);
      },
      error: (err) => {
        console.error('STATUS:', err.status);
        this.errorMensaje.set('No se pudo guardar el iqnuilino. Revisa las validaciones.');
        console.error(err);
        console.log('DATOS PARA CREAR:', this.formInquilino.value);
      },
    });
  }
  SeleccionarEditar(dto: InquilinoReadDto): void {
    this.idInquilinoActualizar.set(dto.idInquilino);
    this.formInquilino.patchValue({
      nombres: dto.nombres,
      identificacion: dto.identificacion,
      email: dto.email,
      telefono: dto.telefono,
    });
  }
  guardar(): void {
    if (this.formInquilino.invalid) {
      console.log('invalido?');
      return;
    }
    const isEditable = this.idInquilinoActualizar();

    if (isEditable != null) {
      this.inquilinoService.updateInquilino(isEditable, this.formInquilino.value).subscribe({
        next: () => {
          this.cargardatos();
          this.cancelarEdicion();
        },
        error: (err) => {
          console.error('Error al actualizar inquilino:', err);
          this.errorMensaje.set('No se pudo actualizar el inquilino.');
        },
      });
    } else {
      this.crear();
    }
  }

  cancelarEdicion(): void {
    this.idInquilinoActualizar.set(null);
    this.formInquilino.reset();
  }

  Desactivar(id: number): void {
    if (confirm('¿Estás seguro de que deseas desactivar este inquilino?')) {
      this.inquilinoService.cambiarEstadoInquilino(id, false).subscribe({
        next: () => {
          this.cargardatos();
          if (this.idInquilinoActualizar() == id) {
            this.cancelarEdicion();
          }
        },
        error: (err) => {
          this.errorMensaje.set('Error al intentar desactivar');
          console.error('error al descativar', err);
        },
      });
    }
  }
}
