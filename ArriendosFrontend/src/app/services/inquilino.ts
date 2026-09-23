import { Injectable,inject } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { InquilinoCreateDto, InmuebleReadDto, InquilinoReadDto} from "../models/arriendos.models";

@Injectable({providedIn:'root'})
export class InquilinoService{

    private http=inject(HttpClient);
    private apiUrl='https://localhost:7237/api/Inquilinos';

getInquilinos(incluirInactivos:boolean=false): Observable<InquilinoReadDto[]> {
  const params={incluirInactivos:incluirInactivos}; 
  return this.http.get<InquilinoReadDto[]>(this.apiUrl,{params});
  }
getInquilinosById(id:number):Observable<InquilinoReadDto>{
    return this.http.get<InquilinoReadDto>(`${this.apiUrl}/${id}`);
}
createInquilino(dto:InquilinoCreateDto):Observable<InquilinoReadDto>{
    return this.http.post<InquilinoReadDto>(this.apiUrl,dto);
}
updateInquilino(id: number, dto: InquilinoCreateDto): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/${id}`, dto);
  }
  cambiarEstadoInquilino(id: number, nuevoEstado: boolean): Observable<any> {
  return this.http.patch<any>(
    `${this.apiUrl}/${id}/cambiar-estado?nuevoEstado=${nuevoEstado}`,
    null
  );
}


}