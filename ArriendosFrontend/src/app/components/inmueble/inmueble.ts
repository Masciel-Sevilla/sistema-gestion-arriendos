import { Component, inject, OnInit, signal, Signal } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators,FormsModule } from '@angular/forms';
import { EdificioReadDto, InmuebleCreateDto, InmuebleReadDto } from '../../models/arriendos.models';

import { InmuebleService } from '../../services/inmueble';
import { EdificioService } from '../../services/edificio';
@Component({
  imports: [ReactiveFormsModule,FormsModule],
  selector: 'app-inmueble',
  standalone: true,
  styleUrl: './inmueble.css',
  templateUrl: './inmueble.html',
})
export class Inmueble implements OnInit {
  private inmubleService = inject(InmuebleService);
  private edificioService = inject(EdificioService);
  private fb = inject(FormBuilder);

  inmuebles = signal<InmuebleReadDto[]>([]);
  edificios = signal<EdificioReadDto[]>([]);
  cargando = signal<boolean>(true);
  errorMensaje = signal<string | null>(null);

  inmuebleEdicionId = signal<number | null>(null);

  filtroEstado = signal<string>('');

  formInmueble: FormGroup = this.fb.group({
    idEdificio: ['', [Validators.required]],
    numeroDepa: ['', [Validators.required, Validators.minLength(2), Validators.maxLength(20)]],
    numeroMedidorLuz: ['', [Validators.required, Validators.maxLength(12)]],
    numeroMedidorAgua: ['', [Validators.required, Validators.maxLength(5)]],
    estado: [{ value: false, disabled: true }],
  });

  ngOnInit(): void {
    this.cargandoDatosIniciales();
  }

  onFiltroEstadoChange(valor: string) {
  this.filtroEstado.set(valor);
  this.cargandoDatosIniciales();
}



  cargandoDatosIniciales(): void {
    this.cargando.set(true);
    const estado=this.filtroEstado() || undefined;

    this.edificioService.getEdificios().subscribe({
      next: (dataEdificios) => {
        this.edificios.set(dataEdificios);
      },
      error: (err) => {
        this.errorMensaje.set('Error cargando Edificios');
        console.error('Error', err);
      },
    });

    this.inmubleService.getInmueble(estado).subscribe({
      next: (data) => {
        this.inmuebles.set(data);
        this.cargando.set(false);
      },
      error: (err) => {
        this.errorMensaje.set('Error al conectar la Api');
        this.cargando.set(false);
        console.error(err);
      },
    });
  }

  seleccionarEditar(inmueble: InmuebleReadDto): void {
    this.inmuebleEdicionId.set(inmueble.idInmueble);
    this.formInmueble.get('estado')?.enable();
    this.formInmueble.patchValue({
      idEdificio: inmueble.idEdificio,
      numeroDepa: inmueble.numeroDepa,
      numeroMedidorLuz: inmueble.numeroMedidorLuz,
      numeroMedidorAgua: inmueble.numeroMedidorAgua,
      estado: inmueble.estado,
    });
  }

  cancelarEdicion(): void {
    this.inmuebleEdicionId.set(null);
    this.formInmueble.get('estado')?.disable();
    this.formInmueble.reset({ idEdificio: '' });
  }
  guardarInmueble(): void {
    if (this.formInmueble.invalid) return;
    const dto = {
      ...this.formInmueble.value,
      idEdificio: Number(this.formInmueble.value.idEdificio),
      estado: false,
    };
    console.log('📌 Payload enviado a la API:', JSON.stringify(dto, null, 2));
    this.inmubleService.createInmueble(dto).subscribe({
      next: (nuevoInmueble) => {
        this.inmuebles.update((lista) => [...lista, nuevoInmueble]);
        this.formInmueble.reset({ idEdificio: '', estado: false });
        this.errorMensaje.set(null);
      },
      error: (err) => {
        this.errorMensaje.set('No se pudo guardar el inmueble. Revisa las validaciones.');
        console.error(err);
      },
    });
  }
  guardar(): void {
    if (this.formInmueble.invalid) return;
    const idEditado = this.inmuebleEdicionId();
    if (idEditado !== null) {
      const dtoUpdate = {
        ...this.formInmueble.value,
        idEdificio: Number(this.formInmueble.value.idEdificio),
      };

      this.inmubleService.updateInmueble(idEditado, dtoUpdate).subscribe({
        next: () => {
          this.cargandoDatosIniciales();

          this.cancelarEdicion();
        },
        error: (err) => {
          console.error('Error al actualizar inmueble:', err);
          this.errorMensaje.set('No se pudo actualizar el inmueble.');
        },
      });
    } else {
      this.guardarInmueble();
    }
  }

  eliminar(id: number): void {
    if (confirm('¿Estás seguro de que deseas inactivar este inmueble?')) {
      this.inmubleService.cambiarEstadoInmueble(id,"INACTIVO").subscribe({
        next: () => {
          this.cargandoDatosIniciales();
          if (this.inmuebleEdicionId() == id) {
            this.cancelarEdicion();
          }
        },
        error: (err) => {
          this.errorMensaje.set('Error al intentar Desactivar');
          console.error('error al dessactivar', err);
        },
      });
    }
  }

  activarInmueble(id:number):void{
    this.inmubleService.cambiarEstadoInmueble(id, 'DISPONIBLE').subscribe({
    next: () => {
      this.cargandoDatosIniciales(); // recarga la tabla para reflejar el cambio
    },
    error: (err) => {
      this.errorMensaje.set('No se pudo activar el inmueble');
      console.error("ERRORR",err);
    }
  });
  
}




  



  getEstadoColor(estado: string): { bg: string; text: string } {
  switch (estado) {
    case 'DISPONIBLE':
      return { bg: '#dcfce7', text: '#15803d' }; // verde
    case 'OCUPADO':
      return { bg: '#fee2e2', text: '#991b1b' }; // rojo
    case 'MANTENIMIENTO':
      return { bg: '#fef9c3', text: '#854d0e' }; // amarillo
    case 'INACTIVO':
      return { bg: '#f3f4f6', text: '#4b5563' }; // gris
    default:
      return { bg: '#f3f4f6', text: '#4b5563' };
  }
}

getEstadoLabel(estado: string): string {
  const labels: Record<string, string> = {
    DISPONIBLE: 'Disponible',
    OCUPADO: 'Ocupado',
    MANTENIMIENTO: 'Mantenimiento',
    INACTIVO: 'Inactivo',
  };
  return labels[estado] ?? estado;
}
}
