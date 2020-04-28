import { Component } from "@angular/core";
import { DataService } from '../shared/dataService';
import { OrderItem } from '../shared/order';

@Component({
    selector: "the-cart",
    templateUrl: "cart.component.html",
    styleUrls: []
})
export class Cart {

    constructor(public data: DataService) {
    }

}