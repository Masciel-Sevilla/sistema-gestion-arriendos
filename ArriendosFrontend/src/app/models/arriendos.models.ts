export interface EdificioReadDto {
  idEdificio: number;
  nombre: string;
  direccion: string;
}

export interface EdificioCreateDto {
  nombre: string;
  direccion: string;
}

export interface InmuebleReadDto {
  idInmueble: number;
  idEdificio: number;
  nombreEdificio?: string;
  numeroDepa: string;
  numeroMedidorLuz?: string;
  numeroMedidorAgua?: string;
  estado: boolean;
}
export interface InmuebleCreateDto {
  idEdificio: number;
  numeroDepa: string;
  numeroMedidorLuz?: string;
  numeroMedidorAgua?: string;
  estado: boolean;
}

export interface InquilinoReadDto {
  idInquilino: number;
  nombres: string;
  identificacion: string;
  email: string;
  telefono: string;
}

export interface InquilinoCreateDto {
  nombres: string;
  identificacion: string;
  email: string;
  telefono: string;
}

export interface ContratoReadDto {
  idContrato: number;
  idInmueble: number;
  numeroDepa: string;
  nombreEdificio: string;
  idInquilino: number;
  nombreInquilino: string;
  identificacionInquilino: string;
  montoArriendo: number;
  montoGarantia: number;
  diaPagoMensual: number;
  fechaInicio: string;
  fechaFin?: string;
  esActivo: boolean;
}

export interface ContratoCreateDto {
  idInmueble: number;
  idInquilino: number;
  montoArriendo: number;
  montoGarantia: number;
  diaPagoMensual: number;
  fechaInicio: string;
  fechaFin?: string;
  esActivo: boolean;
}
