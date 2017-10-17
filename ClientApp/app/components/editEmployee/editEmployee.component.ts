import { Component } from '@angular/core';
import { EmployeeServices } from '../../Services/services';
import { Response } from '@angular/http';
import { Router, ActivatedRoute, Params } from '@angular/router';
import { FormGroup, FormControl, Validators } from '@angular/forms';

@Component({
    selector: 'edit-employee',
    templateUrl: './editEmployee.component.html'
})
export class editEmployeeComponent {
    private EmpId: number;
    public EmployeeDetails = {};
    public employeeName: string;
    public JobTitlesList = [];
    public formData: FormGroup;


    public constructor(private empService: EmployeeServices, private activatedRoute: ActivatedRoute) {
        this.activatedRoute.params.subscribe((params: Params) => {
            this.EmpId = params['id'];
        });

        this.empService.getJobTitlesList()
            .subscribe(
            (data: Response) => (this.JobTitlesList = data.json())
            );

        this.formData = new FormGroup({
            'FirstName': new FormControl('', [Validators.required]),
            'LastName': new FormControl('', [Validators.required]),
            'JobTitle': new FormControl('', Validators.required),
            'EmailAddress': new FormControl('', Validators.required),
            'PhoneNumber': new FormControl('', Validators.required),
            'NationalIdnumber': new FormControl('', Validators.required),
            'BirthDate': new FormControl('', Validators.required)
        });

        this.empService.getEmployeeDetails(this.EmpId)
            .subscribe((data: Response) => (
                this.formData.patchValue({ FirstName: data.json().firstName }),
                this.formData.patchValue({ LastName: data.json().lastName }),
                this.formData.patchValue({ JobTitle: data.json().jobTitle }),
                this.formData.patchValue({ EmailAddress: data.json().emailAddress1 }),
                this.formData.patchValue({ PhoneNumber: data.json().phoneNumber }),
                this.formData.patchValue({ JobTitle: data.json().jobTitle }),
                this.formData.patchValue({ NationalIdnumber: data.json().nationalIdnumber }),
                this.formData.patchValue({ BirthDate: data.json().birthDate })
            ));

    }

    customValidator(control: FormControl): { [s: string]: boolean } {
        if (control.value == "0") {
            return { data: true };
        }
        else {
            return { data: false }
        }
    }

    submitData() {
        if (this.formData.valid) {
            var Obj = {
                FirstName: this.formData.value.FirstName,
                LastName: this.formData.value.LastName,
                JobTitle: this.formData.value.JobTitle,
                EmailAddress: this.formData.value.EmailAddress,
                PhoneNumber: this.formData.value.PhoneNumber,
                NationalIdnumber: this.formData.value.NationalIdnumber,
                BirthDate: this.formData.value.BirthDate,
                BusinessEntityID: this.EmpId
            };
            this.empService.editEmployeeData(Obj)
                .subscribe((data: Response) => (alert("Employee Updated Successfully")));;

       }

    }
}  