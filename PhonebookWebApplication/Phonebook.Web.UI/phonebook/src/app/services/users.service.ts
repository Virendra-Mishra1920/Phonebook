import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { User } from 'src/app/models/app.model';
import { UserResponse } from '../models/UserResponse';
import { map } from 'rxjs/operators';
import { EditUserRequest } from '../models/EditUserRequest';
import { LoginUser } from '../models/UserLogin';
@Injectable({
  providedIn: 'root'
})
export class UsersService {

  baseApiUrl:string=environment.baseApiUrl;

  constructor(private http:HttpClient) {

   }

   getAllUsers():Observable<UserResponse[]>{
    
    return this.http.get<any>(this.baseApiUrl+'/api/user/GetAllUsers').
    pipe(
      
      map(response=>response.users)
    );
   
   }

   addNewUser(addUserRequest:User){
    console.log(typeof addUserRequest)
    return this.http.post(this.baseApiUrl+'/api/user/SaveUser',addUserRequest)
   }

   getUserDetailsById(id:string):Observable<EditUserRequest>
   {
      return this.http.get<any>(this.baseApiUrl+'/api/user/GetUserById/'+id).
      pipe(
          map(response=>response.user)
      )

   }

   updateUser(editUserRequest:EditUserRequest){
    return this.http.put<any>(this.baseApiUrl+ '/api/user/UpdateUser/',editUserRequest).
    pipe(
      map(response=>response.data)
    )
   }

   deleteUser(id:number):Observable<UserResponse>
   {
       return this.http.delete<any>(this.baseApiUrl+'/api/user/DeleteUser/'+id).
       pipe(
        map(response=>response.data)
       )
   }

}
