import { Injectable, Inject } from '@angular/core';
import { Http, Response, Headers, RequestOptions } from "@angular/http";
import 'rxjs/add/operator/toPromise';

@Injectable()
export class DataServices {
    constructor(public http: Http,  @Inject('BASE_URL') public baseUrl: string) {

    }

    getBankAccount(baid: string) {
        return this.http.get(this.baseUrl + 'api/bankaccounts/getbankaccount/' + baid);
    } 

    getBankAccounts() {
        return this.http.get(this.baseUrl + 'api/bankaccounts/get/');
    }

    getCustomers() {
        return this.http.get(this.baseUrl + 'api/customers');
    }

    updateBankAccount(ba: any) {
        let headers = new Headers({ 'Content-Type': 'application/json' });
        let options = new RequestOptions({ headers: headers });
        let body = JSON.stringify(ba);
        return this.http.put(this.baseUrl + 'api/bankaccounts/put', body, options);
    }

    addBankAccount(ba: any) {
        let headers = new Headers({ 'Content-Type': 'application/json' });
        let options = new RequestOptions({ headers: headers });
        let baclass = {
            bankAccountNumber: ba.bankAccountNumber,
            balance: ba.balance,
            customerId: ba.customer
        };
        let body = JSON.stringify(baclass);
        return this.http.post(this.baseUrl + 'api/bankaccounts', body, options);
    }

    private handleError(error: any): Promise<any> {
        console.error('An error occurred', error);
        return Promise.reject(error.message || error);
    }
}

