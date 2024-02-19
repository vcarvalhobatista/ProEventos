import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Evento } from '../models/Evento';

@Injectable(
  // { providedIn: 'root'}
  )
export class EventoService {

constructor(private http: HttpClient) { }

// 'https://pokeapi.co/api/v2/pokemon'
baseUrl = 'https://localhost:5001/api/v1/Eventos';

public getEventos() : Observable<Evento[]>{
  return this.http.get<Evento[]>(this.baseUrl);
}

public getEventosByTema(tema : string) : Observable<Evento[]>{
  return this.http.get<Evento[]>(`${this.baseUrl}/${tema}/tema`);
}

public getEventoById(id : number) : Observable<Evento>{
  return this.http.get<Evento>(`${this.baseUrl}/${id}`);
}
}
