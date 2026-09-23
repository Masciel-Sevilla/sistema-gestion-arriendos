export interface EdificioReadDto {
  idEdificio: number;
  nombre: string;
  direccion: string;
  estado: boolean;
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
  estado: string;
}
export interface InmuebleCreateDto {
  idEdificio: number;
  numeroDepa: string;
  numeroMedidorLuz?: string;
  numeroMedidorAgua?: string;
  estado: string;
}

export interface InquilinoReadDto {
  idInquilino: number;
  nombres: string;
  identificacion: string;
  email: string;
  telefono: string;
  estado: boolean;
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
  estado: string;
}

export interface ContratoCreateDto {
  idInmueble: number;
  idInquilino: number;
  montoArriendo: number;
  montoGarantia: number;
  diaPagoMensual: number;
  fechaInicio: string;
  fechaFin?: string;
  estado: string;
}
export interface CobroMensualReadDto {
  idCobro: number;
  idContrato: number;
  nombreInquilino: string;
  numeroDepa: string;
  mes: number;
  anio: number;
  valorArriendo: number;
  valorLuz: number;
  valorAgua: number;
  saldoAnterior: number;
  totalPagar: number;
  montoPagado: number;
  saldoPendiente: number;
  estado: string;
  fechaUltimoPago: string;
}
export interface CobroMensualCreateDto {
  idContrato: number;
  mes: number;
  anio: number;
  valorArriendo: number;
  valorLuz: number;
  valorAgua: number;
  saldoAnterior: number;
  totalPagar: number;
  montoPagado: number;
  saldoPendiente: number;
  estado: string;
  fechaUltimoPago: string;
}
export interface CobroPagoDto {
  montoAbono: number;
}
