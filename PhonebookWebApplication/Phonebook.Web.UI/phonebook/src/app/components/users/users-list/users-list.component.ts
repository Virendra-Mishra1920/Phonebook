import { Component, OnInit } from '@angular/core';
import { UserResponse } from 'src/app/models/UserResponse';
import { UsersService } from 'src/app/services/users.service';


@Component({
  selector: 'app-users-list',
  templateUrl: './users-list.component.html',
  styleUrls: ['./users-list.component.css']
})
export class UsersListComponent implements OnInit {

  userResponse:UserResponse[]=[]
  constructor(private userService:UsersService) {

    this.userService.getAllUsers()
    .subscribe({
      next:(data)=>{
        this.userResponse=data
        debugger;
        console.log(data)
      }
    })
    
   }

  ngOnInit(): void {

    
  }

}
