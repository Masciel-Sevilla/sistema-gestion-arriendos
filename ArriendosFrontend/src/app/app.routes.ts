import { Routes } from '@angular/router';
import { Edificio } from './components/edificio/edificio';
import { Inmueble } from './components/inmueble/inmueble';
import { Inquilino } from './components/inquilino/inquilino';

export const routes: Routes = [
    {path:'',redirectTo:'edificios',pathMatch:'full'},
    {path:'edificios',component:Edificio},
    { path: 'inmuebles', component:Inmueble },
    { path: 'inquilinos', component:Inquilino },
    { path: 'contratos', children: [] },
    { path: 'cobros', children: [] },
    { path: '**', redirectTo: 'edificios' }

];
