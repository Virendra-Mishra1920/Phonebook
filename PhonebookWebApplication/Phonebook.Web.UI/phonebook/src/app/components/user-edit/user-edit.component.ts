import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { EditUserRequest } from 'src/app/models/EditUserRequest';
import { UsersService } from 'src/app/services/users.service';

@Component({
  selector: 'app-user-edit',
  templateUrl: './user-edit.component.html',
  styleUrls: ['./user-edit.component.css']
})
export class UserEditComponent implements OnInit {

  editUserRequest:EditUserRequest={
    id:0,
    firstName:'',
    lastName:'',
    email:'',
    phone:''
  }

  constructor(private route:ActivatedRoute, private service:UsersService, private router:Router) { }

  ngOnInit(): void {
    // get the id from params
    this.route.paramMap.subscribe({

      next:(params)=>{
        const id=params.get('id');
        this.editUserRequest.id=Number(id)
        if(id)
        {
          this.service.getUserDetailsById(id).subscribe({
            next:(response)=>{
              this.editUserRequest=response
            }
          })
        }
      }
    })
  }

  updateUser(){
    this.service.updateUser(this.editUserRequest).subscribe({
      next:(response)=>{
        this.router.navigate(['users'])
      }
    })
  }

  deleteUser(id:number){
    this.service.deleteUser(id).subscribe({
      next:(response)=>{
        this.router.navigate(['users'])
      }
    })
  }

}
