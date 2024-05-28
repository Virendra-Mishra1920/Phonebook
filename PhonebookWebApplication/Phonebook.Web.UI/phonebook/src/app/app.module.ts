import { ErrorHandler, NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { UsersListComponent } from './components/users/users-list/users-list.component';
import {HttpClientModule, HTTP_INTERCEPTORS} from '@angular/common/http';
import { AddUserComponent } from './components/add-user/add-user.component';
import { FormsModule } from '@angular/forms';
import { UserDeleteComponent } from './components/user-delete/user-delete.component';
import { UserEditComponent } from './components/user-edit/user-edit.component';
import { NavBarComponent } from './components/nav-bar/nav-bar.component';
import { ErrorPageComponent } from './components/error-page/error-page.component';
import { GlobalErrorHandler } from './services/global-error-handler.service';

@NgModule({
  declarations: [
    AppComponent,
    UsersListComponent,
    AddUserComponent,
    UserDeleteComponent,
    UserEditComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule
  ],
  providers: [
    {
      provide:ErrorHandler,
      useClass:GlobalErrorHandler
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
