import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { RouterModule } from '@angular/router';
import { DataTableModule } from 'primeng/primeng'; // Here
import { PaginatorModule } from 'primeng/primeng'; // Here

import { AppComponent } from './components/app/app.component';
import { NavMenuComponent } from './components/navmenu/navmenu.component';
import { HomeComponent } from './components/home/home.component';
import { BankAccountComponent } from './components/bankaccount/bankaccount.component';

import { DataServices } from './services/data.service';


@NgModule({
    declarations: [
        AppComponent,
        NavMenuComponent,
        BankAccountComponent,
        HomeComponent
    ],
    imports: [
        CommonModule,
        HttpModule,
        FormsModule, 
        ReactiveFormsModule,
        DataTableModule,
        PaginatorModule,
        RouterModule.forRoot([
            { path: '', redirectTo: 'home', pathMatch: 'full' },
            { path: 'home', component: HomeComponent },
            { path: 'bankaccount', component: BankAccountComponent },
            { path: '**', redirectTo: 'home' }
        ])
    ],
    providers: [DataServices],
    bootstrap: [AppComponent]
})
export class AppModuleShared {
}
