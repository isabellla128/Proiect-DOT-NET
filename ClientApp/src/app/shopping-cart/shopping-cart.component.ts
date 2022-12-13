import { Component, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Medication } from 'src/models/medication';
import { ShoppingCartService } from 'src/shared/services/shopping-cart.service';

@Component({
  selector: 'app-shopping-cart',
  templateUrl: './shopping-cart.component.html',
  styleUrls: ['./shopping-cart.component.css'],
})
export class ShoppingCartComponent implements OnInit {
  items: { count: number; medication: Medication }[] = [];
  totalPrice: number = 0;

  constructor(
    private shoppingCartService: ShoppingCartService,
    private snackBar: MatSnackBar
  ) {}

  ngOnInit(): void {
    this.shoppingCartService.$items.subscribe((items) => (this.items = items));
    this.shoppingCartService.$totalPrice.subscribe(
      (price) => (this.totalPrice = +price.toFixed(2))
    );
  }

  deleteFromCart(medicationId: string) {
    this.shoppingCartService.delete(medicationId);
  }

  onPay() {
    this.shoppingCartService.cleanCart();
    this.snackBar.open('Thank You!', 'Confirm', {
      horizontalPosition: 'center',
      verticalPosition: 'bottom',
    });
  }
}
