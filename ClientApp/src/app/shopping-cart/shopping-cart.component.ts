import { Component, Inject, OnInit, SecurityContext } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { DomSanitizer } from '@angular/platform-browser';
import { DOCUMENT } from '@angular/common';
import { Medication } from 'src/models/medication';
import { CURRENCIES, RegisterDoParams } from 'src/models/payment';
import { PaymentService } from 'src/shared/services/payment.service';
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
    @Inject(DOCUMENT) private document: Document,
    private shoppingCartService: ShoppingCartService,
    private snackBar: MatSnackBar,
    private paymentService: PaymentService,
    public domSanitizerService: DomSanitizer
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
    if (this.totalPrice !== 0) {
      console.log(this.totalPrice);

      const paymentParams: RegisterDoParams = {
        orderNumber: Math.floor(Math.random() * 10000000).toString(),
        amount: this.totalPrice,
        currency: CURRENCIES.RON,
        returnUrl: 'http://localhost:4200/payment',
        description: 'testing',
        pageView: 'DESKTOP',
        language: 'ro',
        jsonParams: { FORCE_3DS2: false },
        orderBundle: {
          orderCreationDate: new Date().toISOString().split('T')[0],
          // customerDetails,
        },
      };
      this.paymentService
        .getRegisterDoResponse(paymentParams)
        .subscribe((result) => {
          const safeUrl = this.domSanitizerService.sanitize(
            SecurityContext.RESOURCE_URL,
            this.domSanitizerService.bypassSecurityTrustResourceUrl(
              result.formUrl
            )
          );
          this.document.location.href = safeUrl || 'localhost:4200';
        });

      this.shoppingCartService.cleanCart();
    } else {
      this.snackBar.open('The shopping cart is empty', 'Ok!', {
        horizontalPosition: 'center',
        verticalPosition: 'bottom',
      });
    }
  }
}
