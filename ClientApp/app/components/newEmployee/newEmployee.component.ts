import { Component } from '@angular/core';
import { EmployeeServices } from '../../Services/services';
import { Response } from '@angular/http';
import { FormGroup, FormControl, Validators } from '@angular/forms';  

@Component({
    selector: 'new-employee',
    templateUrl: './newEmployee.component.html'
})
export class newEmployeeComponent {
    public JobTitlesList = [];
    public formData: FormGroup;
    public constructor(private empService: EmployeeServices) {
        this.empService.getJobTitlesList()
            .subscribe(
            (data: Response) => (this.JobTitlesList = data.json())
            );

        this.formData = new FormGroup({
            'FirstName': new FormControl('', [Validators.required]),
            'LastName': new FormControl('', [Validators.required]),
            'JobTitle': new FormControl('', Validators.required),
            'Email': new FormControl('', Validators.required),
            'Phone': new FormControl('', Validators.required)          
        });
    }
    customValidator(control: FormControl): { [s: string]: boolean } {
        if (control.value == "0") {
            return { data: true };
        }
        else {
            return { data: false };
        }
    }

    submitData() {
        if (this.formData.valid) {
            var Obj = {
                FirstName: this.formData.value.FirstName,
                LastName: this.formData.value.LastName,
                JobTitle: this.formData.value.JobTitle,                
                Email: this.formData.value.Email,
                Phone: this.formData.value.Phone,
            };
            this.empService.postData(Obj).subscribe();
            alert("Employee Inserted Successfully");
        }

    }
}
