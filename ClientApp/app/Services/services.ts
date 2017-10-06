import { Injectable } from '@angular/core';
import { Http, RequestOptions, Headers } from '@angular/http';

@Injectable()
export class EmployeeServices {
    constructor(private http: Http) {

    }
    getEmployeeList() {
        return this.http.get('http://localhost:62362/api/employee');
    }
    getJobTitlesList() {
        return this.http.get('http://localhost:62362/api/jobtitles');
    }

    postData(empObj: any) {
        let headers = new Headers({
            'Content-Type':
            'application/json; charset=utf-8'
        });
        let options = new RequestOptions({ headers : headers });
        return this.http.post('http://localhost:54273/api/employee', JSON.stringify(empObj), options);
    }  
}   