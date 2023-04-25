import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { User } from '../shared/models/user';
import { BehaviorSubject, ReplaySubject, map, of } from 'rxjs';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  baseUrl = environment.apiUrl;
  private currentUserSource = new BehaviorSubject<User | null>(null);
  currentUser$ = this.currentUserSource.asObservable();

  constructor(private http: HttpClient, private router: Router) { }

  login(values: any) {
    return this.http.post<User>(this.baseUrl + 'authentication/login', values).pipe(
      map((response: User) => {
        const user = response;
        if (user) {
          this.setCurrentUser(user);
        }
      })
    )
  }

  logout() {
    localStorage.removeItem('token');
    this.currentUserSource.next(null);
    this.router.navigateByUrl('/');
  }

  register(values: any) {
    return this.http.post<User>(this.baseUrl + 'authentication/register', values).pipe(
      map(user => {
        if (user) {
          this.setCurrentUser(user);
        }
      })
    )
  }

  setCurrentUser(user: User) {
    localStorage.setItem('token', JSON.stringify(user));
    this.currentUserSource.next(user);
  }

  checkEmailExists(email: string) {
    return this.http.get<boolean>(this.baseUrl + 'authentication/emailexists?email=' + email);
  }
}
