import { Component } from '@angular/core';
import { EmployeeServices } from '../../Services/services';
import { Response } from '@angular/http';
import { Router, ActivatedRoute, Params } from '@angular/router';

@Component({
    selector: 'employee-detail',
    templateUrl: './details.component.html'
})
export class DetailsComponent {
    private EmpId: number;
    public EmployeeDetails: { [index: string]: string } = {};
    public constructor(private empService: EmployeeServices, private activatedRoute: ActivatedRoute) {
        this.activatedRoute.params.subscribe((params: Params) => {
            this.EmpId = params['id'];
        });

        this.empService.getEmployeeDetails(this.EmpId)
            .subscribe((data: Response) => (
                this.EmployeeDetails["BusinessEntityID"] = data.json().businessEntityID,
                this.EmployeeDetails["LastName"] = data.json().lastName,
                this.EmployeeDetails["FirstName"] = data.json().firstName,
                this.EmployeeDetails["JobTitle"] = data.json().jobTitle,
                this.EmployeeDetails["EmailAddress"] = data.json().emailAddress1,
                this.EmployeeDetails["PhoneNumber"] = data.json().phoneNumber,
                this.EmployeeDetails["NationalIdnumber"] = data.json().nationalIdnumber,
                this.EmployeeDetails["BirthDate"] = data.json().birthDate
            ));

    }

}  