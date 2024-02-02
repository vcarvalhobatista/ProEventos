import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { of } from 'rxjs';
import { catchError, map } from 'rxjs/operators';

@Component({
  selector: 'app-eventos',
  templateUrl: './eventos.component.html',
  styleUrls: ['./eventos.component.css']
})
export class EventosComponent implements OnInit {
  constructor(private http: HttpClient) {}

  public eventos: any;

  ngOnInit(): void {
    this.getEventos();
  }

  public getEventos(): void {
    this.http.get<any[]>('https://localhost:5001/api/Eventos')
      .pipe(
        map(response => this.eventos = response),
        catchError(error => {
          console.error(error);
          return of([]); // ou qualquer valor padrão desejado
        })
      )
      .subscribe();
  }
}
