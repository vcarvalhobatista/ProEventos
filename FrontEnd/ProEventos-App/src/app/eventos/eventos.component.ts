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

  
  isCollapsed = true;
  widthImg = 150;
  marginImg = 2;
  private _filtroLista = "";
  public eventos: any = [];
  public eventosFiltrados : any = [];
  
  public get filtroLista(){
    return this._filtroLista;
  }
  
  public set filtroLista(value : string){
    this._filtroLista = value;
    this.eventosFiltrados = this.filtroLista ? this.filtrarEventos(this._filtroLista) : this.eventos
  }
  
  filtrarEventos(filtrarPor : string) : any
  {
    filtrarPor = filtrarPor.toLowerCase();
    
    return this.eventos.filter(
      (evento : any) => evento.tema.toLowerCase().indexOf(filtrarPor) !== -1 || 
      evento.local.toLowerCase().indexOf(filtrarPor) !== -1
      // ,
      // console.log(filtrarPor)
      )
    }
    
  constructor(private http: HttpClient) {}
  
  ngOnInit(): void {
    this.getEventos();
  }

  showImage(){
    this.isCollapsed = !this.isCollapsed;
  }

  public getEventos(): void {
    this.http.get<any[]>('https://localhost:5001/api/Eventos')
      .pipe(
        map(response => {
          this.eventos = response;
          this.eventosFiltrados = this.eventos;
        }),
        catchError(error => {
          console.error(error);
          return of([]); // ou qualquer valor padrão desejado
        })
      )
      .subscribe();
  }
}
