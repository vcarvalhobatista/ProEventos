import { Component, OnInit } from '@angular/core';
import { of } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { EventoService } from '../services/evento.service';
import { Evento } from '../models/Evento';
import { BsModalRef } from 'ngx-bootstrap/modal';

@Component({
  selector: 'app-eventos',
  templateUrl: './eventos.component.html',
  styleUrls: ['./eventos.component.css']
  // ,providers: [EventoService]
})
export class EventosComponent implements OnInit {
  modalRef : BsModalRef;
  
  public isCollapsed = true;
  public widthImg = 150;
  public marginImg = 2;
  private _filtroLista = "";
  public eventos: Evento[] = [];
  public eventosFiltrados : Evento[] = [];
  
  public get filtroLista() {
    return this._filtroLista;
  }
  
  public set filtroLista(value : string){
    this._filtroLista = value;
    this.eventosFiltrados = this.filtroLista ? this.filtrarEventos(this._filtroLista) : this.eventos;
  }
  
  public filtrarEventos(filtrarPor : string) : any
  {
    filtrarPor = filtrarPor.toLowerCase();
    
    return this.eventos.filter(
      (evento : Evento) => evento.tema.toLowerCase().indexOf(filtrarPor) !== -1 || 
      evento.local.toLowerCase().indexOf(filtrarPor) !== -1
      // ,
      // console.log(filtrarPor)
      );
    }
    
  constructor(private eventoService : EventoService) {}
  
  public ngOnInit(): void {
    this.getEventos();
  }

  public showImage(){
    this.isCollapsed = !this.isCollapsed;
  }

  public getEventos(): void {
    this.eventoService.getEventos()
      .pipe(
        map((_eventos: Evento[]) => {
          this.eventos = _eventos;
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
