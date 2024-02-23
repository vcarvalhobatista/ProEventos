import { Component, OnInit, TemplateRef } from '@angular/core';
import { BsModalService } from 'ngx-bootstrap/modal';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { map, catchError, of } from 'rxjs';
import { Evento } from 'src/app/models/Evento';
import { EventoService } from 'src/app/services/evento.service';

@Component({
  selector: 'app-evento-lista',
  templateUrl: './evento-lista.component.html',
  styleUrls: ['./evento-lista.component.css']
})
export class EventoListaComponent implements OnInit {


  public isCollapsed = true;
  public widthImg = 150;
  public marginImg = 2;
  private _filtroLista = "";
  public eventos: Evento[] = [];
  public eventosFiltrados : Evento[] = [];
  public modalRef : any;
  public message?: string;
    
  constructor(private eventoService : EventoService, 
              private modalService: BsModalService,
              private toastr: ToastrService,
              private spinner: NgxSpinnerService) {}
  
  openModal(template: TemplateRef<void>) : void{
    this.modalRef = this.modalService.show(template, { class: 'modal-sm' });
  }
  
  confirm(): void {    
    this.toastr.success('Evento deletado com Sucesso.', 'Deletado!');
    this.modalRef?.hide();
  }
  
  decline(): void {    
    this.modalRef?.hide();
  }

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
    
  
  public ngOnInit(): void {
    
    this.getEventos();
    
  }

  public showImage(){
    this.isCollapsed = !this.isCollapsed;
  }

  public getEventos(): void {
    this.spinner.show();
    this.eventoService.getEventos()
      .pipe(
        map((_eventos: Evento[]) => {
          this.eventos = _eventos;
          this.eventosFiltrados = this.eventos;
        }),
        catchError(error => {
          console.error(error);
          this.toastr.error('Erro ao carregar os Eventos', 'Erro!');
          this.spinner.hide();
          return of([]); // ou qualquer valor padrão desejado
        })                
      )
      .subscribe();

      setTimeout(() => {
        /** spinner ends after 5 seconds */
        this.spinner.hide();
      }, 1500);
  }

}
