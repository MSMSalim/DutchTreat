import { Component } from "@angular/core";
import { Router } from "@angular/router";
import { DataService } from '../../shared/dataService';

@Component({
    selector: "login",
    templateUrl: "login.component.html"
})
export class Login {

    constructor(public data: DataService, private router: Router) {
    }

    public creds = {
        username: "",
        password: ""
    };

    onLogin() {
        //call the logging service
    }

}