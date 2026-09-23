import { Injectable,inject } from "@angular/core";
import { Observable } from "rxjs";
import { HttpClient, HttpParams } from "@angular/common/http";
import { CobroMensualCreateDto,CobroMensualReadDto,CobroPagoDto } from "../models/arriendos.models";

@Injectable({providedIn:'root'})

export class CobroService{
private http=inject(HttpClient);
private apiUrl = 'https://localhost:7237/api/CobrosMensuales';

getCobros(mes?:number,anio?:number,esPagado?:boolean,idContrato?:number):Observable<CobroMensualReadDto[]>{
    let params= new HttpParams();

    if(mes) params=params.set('mes',mes);
    if (anio) params = params.set('anio', anio);
    if (esPagado !== undefined && esPagado !== null) params = params.set('esPagado', esPagado);
    if (idContrato) params = params.set('idContrato', idContrato);
    return this.http.get<CobroMensualReadDto[]>(this.apiUrl,{params});
}
getCobroById(id: number): Observable<CobroMensualReadDto> {
    return this.http.get<CobroMensualReadDto>(`${this.apiUrl}/${id}`);
  }

  
  createCobro(dto: CobroMensualCreateDto): Observable<CobroMensualReadDto> {
    return this.http.post<CobroMensualReadDto>(this.apiUrl, dto);
  }
  updateCobro(id: number, dto: CobroMensualCreateDto): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/${id}`, dto);
  }

  registrarPago(id: number,dto:CobroPagoDto):Observable<CobroMensualReadDto>{
    return this.http.put<CobroMensualReadDto>(`${this.apiUrl}/${id}/registrar-pago`,dto);
  }

  deleteCobro(id:number):Observable<void>{
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }

}