import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './components/app/app.component';
import { NavMenuComponent } from './components/navmenu/navmenu.component';
import { HomeComponent } from './components/home/home.component';
import { DetailsComponent } from './components/details/details.component';
import { newEmployeeComponent } from './components/newEmployee/newEmployee.component';
import { editEmployeeComponent } from './components/editEmployee/editEmployee.component';
import { EmployeeServices } from './Services/services';
@NgModule({
    declarations: [
        AppComponent,
        NavMenuComponent,
        DetailsComponent,
        newEmployeeComponent,
        editEmployeeComponent,
        HomeComponent
    ],
    providers: [EmployeeServices, HttpModule],
    imports: [
        CommonModule,
        HttpModule,
        FormsModule,
        RouterModule.forRoot([
            { path: '', redirectTo: 'home', pathMatch: 'full' },
            { path: 'home', component: HomeComponent },
            { path: 'details/:id', component: DetailsComponent },
            { path: 'new', component: newEmployeeComponent },
            { path: 'edit/:id', component: editEmployeeComponent },
            { path: '**', redirectTo: 'home' }
        ])
    ]
})
export class AppModuleShared {
}