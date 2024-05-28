import { ErrorHandler, Injectable, Injector } from '@angular/core';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class GlobalErrorHandler implements ErrorHandler {

  constructor(private injector:Injector) { }
  handleError(error: any): void {
    const router=this.injector.get(Router)

    console.error("An error occured", error)

    router.navigate(['/error'])
  }

  
}
