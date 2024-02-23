import { Component, Input, OnInit } from '@angular/core';
import { NgModule } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-title',
  templateUrl: './title.component.html',
  styleUrls: ['./title.component.css']
})

export class TitleComponent implements OnInit {
  @Input() titulo!: string;
  @Input() iconClass = "fa fa-user";
  @Input() subtitulo = 'Desde 2023';
  @Input() btnListar = false;
  constructor(private route: Router) { }

  ngOnInit() {}

  listar() : void {
    this.route.navigate([`/${this.titulo.toLocaleLowerCase()}/lista`]);
  }

}
