import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { AppSettings } from 'app/app.config';
import { map } from 'rxjs/operators';
import {Router} from "@angular/router";


@Injectable()
export class AuthenticationService  {

    private api: string;

    constructor(private http: HttpClient, private router: Router) {
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
        this.router.navigate(['/login']);
    }

    public isAuthenticated(): any {
        return !!localStorage.getItem('currentUser');
    }

    public getCurrentUser(): any {
        return localStorage.getItem('currentUser') ? JSON.parse(localStorage.getItem('currentUser')) : null;
    }

    public userRole(): any {
        return this.getCurrentUser() ? this.getCurrentUser().role : null;
    }

    public userHasRole(role: string) {
        return this.getCurrentUser() && this.getCurrentUser().role.toLowerCase() === role.toLowerCase();
    }
}
