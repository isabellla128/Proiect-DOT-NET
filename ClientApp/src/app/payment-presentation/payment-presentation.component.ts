import { Component, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute } from '@angular/router';
import { OrderStatusResponse } from 'src/models/payment';
import { PaymentService } from 'src/shared/services/payment.service';

@Component({
  selector: 'app-payment-presentation',
  templateUrl: './payment-presentation.component.html',
  styleUrls: ['./payment-presentation.component.css'],
})
export class PaymentPresentationComponent implements OnInit {
  response: OrderStatusResponse = {} as OrderStatusResponse;

  constructor(
    private route: ActivatedRoute,
    private paymentService: PaymentService,
    private snackBar: MatSnackBar
  ) {}

  ngOnInit() {
    this.route.queryParamMap.subscribe((params) => {
      this.paymentService
        .getOrderStatusExtended({
          orderId: params.get('orderId') || '',
        })
        .subscribe((response) => {
          this.response = response;
          if (response.actionCode !== 0)
            this.snackBar.open('The payment did not succeed', 'Ok!', {
              horizontalPosition: 'center',
              verticalPosition: 'bottom',
            });
          else {
            this.paymentService.savePayment({
              payment: response,
            });
          }
        });
    });
  }
}
