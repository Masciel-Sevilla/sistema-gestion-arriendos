import { Injectable,inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { EdificioCreateDto,EdificioReadDto } from '../models/arriendos.models';

@Injectable({providedIn:'root'})
export class EdificioService{
    private http=inject(HttpClient);
    private apiUrl='https://localhost:7237/api/Edificios';

    getEdificios(incluirInactivos:boolean=false):Observable<EdificioReadDto[]>{
        const params={incluirInactivos:incluirInactivos};
        
          return this.http.get<EdificioReadDto[]>(this.apiUrl, { params });  }

    getEdificioById(id:number):Observable<EdificioReadDto>{
        return this.http.get<EdificioReadDto>(`${this.apiUrl}/${id}`);
    }

    createEdificio(dto:EdificioCreateDto):Observable<EdificioReadDto>{
        return this.http.post<EdificioReadDto>(this.apiUrl,dto);
    }
    updateEdificio(id:number,dto:EdificioCreateDto):Observable<void>{
        return this.http.put<void>(`${this.apiUrl}/${id}`,dto);
    }
    cambiarEstadoEdificio(id: number,nuevoEstado:boolean): Observable<any> {
    return this.http.patch<any>(`${this.apiUrl}/${id}/cambiar-estado`,nuevoEstado);
  }

}