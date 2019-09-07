import { Component } from '@angular/core';
import {AuthGuard} from "./infrastructure/guards/auth.guard";

@Component({
    selector: 'app-root',
    templateUrl: './app.html',
    providers: [AuthGuard]
})
export class AppComponent {}
