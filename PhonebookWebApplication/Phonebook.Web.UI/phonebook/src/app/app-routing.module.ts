import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { UsersListComponent } from './components/users/users-list/users-list.component';
import { AddUserComponent } from './components/add-user/add-user.component';
import { UserDeleteComponent } from './components/user-delete/user-delete.component';
import { UserEditComponent } from './components/user-edit/user-edit.component';
import { ErrorPageComponent } from './components/error-page/error-page.component';

const routes: Routes = [
  {
    path:'',
    component: AddUserComponent
  },
  {
    path:'users',
    component: UsersListComponent
  },
  {
    path:'users/add',
    component: AddUserComponent
  },
  {
    path:'users/delete',
    component: UserDeleteComponent
  },
  {
    path:'users/edit/:id',
    component: UserEditComponent
  },

  
  {
    path:'error',
    component: ErrorPageComponent
  },

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
