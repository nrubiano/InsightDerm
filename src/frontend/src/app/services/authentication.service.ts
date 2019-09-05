import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { AppSettings } from 'app/app.config';
import { map } from 'rxjs/operators';


@Injectable()
export class AuthenticationService  {

    private api: string;

    constructor(private http: HttpClient) {
        this.api = AppSettings.API + '/users';
     }

    login(username: string, password: string) {
        return this.http.post<any>(`${this.api}/authenticate`, { userName: username, password: password }, {})
        .pipe(map(user => {
            if (user && user.token) {
                localStorage.setItem('currentUser', JSON.stringify(user));
            }

            return user;
        }));
    }

    logout() {
        localStorage.removeItem('currentUser');
    }
}
