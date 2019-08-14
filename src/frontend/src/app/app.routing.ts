import { Routes, RouterModule } from '@angular/router';
import { ModuleWithProviders } from '@angular/core';
import {PagesLoginAlpha} from "./structure/pages/login-alpha.page";
import {PagesRegister} from "./structure/pages/register.page";

export const routes: Routes = [
    { path: '', redirectTo: 'login', pathMatch: 'full' },
    { path: '**', redirectTo: 'pages/page-404' }

];

export const routing: ModuleWithProviders = RouterModule.forRoot(routes, { useHash: true });
