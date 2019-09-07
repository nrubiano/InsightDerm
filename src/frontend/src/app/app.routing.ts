import { Routes, RouterModule } from '@angular/router';
import { ModuleWithProviders } from '@angular/core';
import {Roles} from "./models/base";
import {AuthGuard} from "./infrastructure/guards/auth.guard";

export const routes: Routes = [
    { path: '', redirectTo: 'dashboard', pathMatch: 'full'},
    { path: '**', redirectTo: 'pages/page-404' }

];

export const routing: ModuleWithProviders = RouterModule.forRoot(routes, { useHash: true });
