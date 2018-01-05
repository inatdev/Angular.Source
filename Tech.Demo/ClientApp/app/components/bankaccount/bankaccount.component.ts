import { Component, OnInit } from '@angular/core';
import { DataServices } from '../../services/data.service';
import { DataTableModule, SharedModule } from 'primeng/primeng';
import 'rxjs/add/operator/toPromise';
import 'rxjs/add/operator/map';
import { FormGroup, FormControl, FormBuilder, Validators } from '@angular/forms';

@Component({
    selector: 'bankaccount',
    templateUrl: './bankaccount.component.html'
})
export class BankAccountComponent implements OnInit {
    public myForm: FormGroup; 
    private customers: Customer[];
    public bankaccounts: BankAccount[];
    public bankAccount: BankAccount;
    constructor(private dataService: DataServices, private _fb: FormBuilder) {
    }

    ngOnInit() {
        this.dataService.getBankAccounts().subscribe((bc) => {
            this.bankaccounts = bc.json() as BankAccount[];
        });
        this.dataService.getCustomers().subscribe((bc) => {
            this.customers = bc.json() as Customer[];
        });
        this.myForm = this._fb.group({
            customer: ['']
        });

    }

    onEditComplete(ev: any) {
        //Everything is fine here.  Show's selected value of the pulldown.
        console.log(ev);        
        this.dataService.updateBankAccount(ev).subscribe();
    }

    save(model: BankAccount, isValid: boolean) {
        this.dataService.addBankAccount(model).subscribe((bc) => {
            this.updateGrid(bc.json() as BankAccount);
        });

        //this.dataService.getBankAccount(bankAccount.id).subscribe((bc) => {
        //    this.bankaccounts = bc.json() as BankAccount[];
        //});
    }

    updateGrid(model: BankAccount) {
        console.log(model);
        this.bankaccounts = [...this.bankaccounts, model];
    }
}
interface Customer {
    id: string;
    firstName: string;
    lastName: string;
}
interface BankAccountNum {
    checkDigits: string;
    nationalBankCode: string;
    officeNumber: string;
    accountNumber: string;
}
interface BankAccount {
    bankAccountNumber: BankAccountNum;
    balance: number;
    locked: boolean;
    customer: Customer;
    id: string;
}


