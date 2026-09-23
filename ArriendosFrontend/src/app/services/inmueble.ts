import { Injectable,inject } from "@angular/core";
import { Observable } from "rxjs";
import { HttpClient } from "@angular/common/http";
import { HttpParams } from "@angular/common/http";
import { InmuebleReadDto,InmuebleCreateDto } from "../models/arriendos.models";

@Injectable({providedIn:'root'})
export class InmuebleService{
    private http=inject(HttpClient);
    private apiUrl='https://localhost:7237/api/Inmuebles';

    getInmueble(estado?:string):Observable<InmuebleReadDto[]>{
        let params = new HttpParams();
  if (estado) {
    params = params.set('estado', estado);
  }
        
        return this.http.get<InmuebleReadDto[]>(this.apiUrl,{params});
    }

    getInmuebleById(id:number):Observable<InmuebleReadDto>{
        return this.http.get<InmuebleReadDto>(`${this.apiUrl}/${id}`);
    }

    createInmueble(dto:InmuebleCreateDto):Observable<InmuebleReadDto>{

        return this.http.post<InmuebleReadDto>(this.apiUrl,dto);
    }

    updateInmueble(id: number, dto: InmuebleCreateDto): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/${id}`, dto);
  }


  
  cambiarEstadoInmueble(id: number, estado: string) {
  return this.http.patch(
    `${this.apiUrl}/${id}/cambiar-estado?estado=${estado}`,
    null
  );

}
}