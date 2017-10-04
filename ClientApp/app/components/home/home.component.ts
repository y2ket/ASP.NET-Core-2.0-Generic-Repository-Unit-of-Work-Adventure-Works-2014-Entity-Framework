import { Component } from '@angular/core';
import { EmployeeServices } from "../../Services/services";
import { Response } from '@angular/http';  
@Component({
    selector: 'home',
    templateUrl: './home.component.html'
})
export class HomeComponent {
    public EmployeeList = [];
    public constructor(private empService: EmployeeServices) {
        this.empService.getEmployeeList()
            .subscribe(
            (data: Response) => (this.EmployeeList = data.json())
            );
    }
}