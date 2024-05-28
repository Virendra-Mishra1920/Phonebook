import { Component, ErrorHandler, Injector, OnInit } from '@angular/core';
import { Router } from '@angular/router';


@Component({
  selector: 'app-error-page',
  templateUrl: './error-page.component.html',
  styleUrls: ['./error-page.component.css']
})
export class ErrorPageComponent implements OnInit {
  errorStatus:number=0;
  errorMessage:string=''

  constructor() { }
  
  ngOnInit(): void {
    
  }

}
