import { Component, OnInit } from '@angular/core';
import { Route, Router } from '@angular/router';
import { User } from 'src/app/models/app.model';
import { UsersService } from 'src/app/services/users.service';

@Component({
  selector: 'app-add-user',
  templateUrl: './add-user.component.html',
  styleUrls: ['./add-user.component.css']
})
export class AddUserComponent implements OnInit {

  hobbies:string='';

  constructor(private userService:UsersService, private route:Router) { }

  addUserRequest:User={
    userId:0,
    firstName:'',
    lastName:'',
    email:'',
    phone:''
    
  }


  ngOnInit(): void {

  }

  addUser(){
    let hobbies: string[] = this.hobbies.split(',').map(item => item.trim());
    this.userService.addNewUser(this.addUserRequest).subscribe({
      next:(data)=>{
        this.route.navigate(['users'])

      }
    })
  }

}
