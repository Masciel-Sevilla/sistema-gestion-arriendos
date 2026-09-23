import { Injectable,inject } from "@angular/core";
import { Observable } from "rxjs";
import { HttpClient, HttpParams } from "@angular/common/http";
import { ContratoCreateDto,ContratoReadDto } from "../models/arriendos.models";

@Injectable({providedIn:'root'})

export class ContratoService{

    private http=inject(HttpClient);
    private apiUrl='https://localhost:7237/api/Contratos';

    getContratos(esActivo?:boolean,idInquilino?:number,idInmueble?:number):Observable<ContratoReadDto[]>{

        let params=new HttpParams();
        if(esActivo!=undefined && esActivo!=null){
            params=params.set('esActivo',esActivo);
        }

        if(idInmueble){
            params=params.set('idInmueble',idInmueble);
        }
        if (idInquilino) {
      params = params.set('idInquilino', idInquilino);
    }
    return this.http.get<ContratoReadDto[]>(this.apiUrl,{params});

    }

    getContratoById(id: number): Observable<ContratoReadDto> {
    return this.http.get<ContratoReadDto>(`${this.apiUrl}/${id}`);
  }
createContrato(dto: ContratoCreateDto): Observable<ContratoReadDto> {
    return this.http.post<ContratoReadDto>(this.apiUrl, dto);
  }

  updateContrato(id:number,dto:ContratoCreateDto):Observable<void>{
return this.http.put<void>(`${this.apiUrl}/${id}`,dto)

  }

  cancelarContrato(id:number):Observable<{mensaje:string}>{

    return this.http.put<{mensaje:string}>(`${this.apiUrl}/${id}/cancelar`,{})
  }

  deleteContrato(id:number):Observable<void>{
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }

}