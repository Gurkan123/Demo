import { Injectable } from '@angular/core';
import { HttpHeaders, HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Perm } from '../_models/perm';
import { Observable } from 'rxjs';


const httpOptions = {
  headers: new HttpHeaders({
    'Authorization': 'Bearer ' + localStorage.getItem('token')
  })
};

@Injectable({
  providedIn: 'root'
})
export class RoleService {
  baseUrl = environment.apiUrl;

constructor(private http: HttpClient) { }

getPerms(): Observable<Perm[]> {
  return this.http.get<Perm[]>(this.baseUrl + 'perm', httpOptions);
}

deletePerm(id: number){
  return this.http.delete<Perm[]>(this.baseUrl + 'perm/' + id, httpOptions);
}

postPerm(model: any): Observable<any> {
  return this.http.post(this.baseUrl + 'perm', model, httpOptions);
}

postRole(model: any): Observable<any> {
  return this.http.post(this.baseUrl + 'role/role', model, httpOptions);
}

postRolePerm(model: any): Observable<any> {
  return this.http.post(this.baseUrl + 'role/roleperm', model, httpOptions);
}

}
