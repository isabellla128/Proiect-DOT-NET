import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NavbarComponent } from './navbar/navbar.component';
import { LayoutModule } from '@angular/cdk/layout';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatButtonModule } from '@angular/material/button';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatIconModule } from '@angular/material/icon';
import { MatListModule } from '@angular/material/list';
import { MatCardModule } from '@angular/material/card';
import { MatGridListModule } from '@angular/material/grid-list';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { MatMenuModule } from '@angular/material/menu';
import { MatExpansionModule } from '@angular/material/expansion';
import { HomeComponent } from './home/home.component';
import { MedicCardComponent } from './medic-card/medic-card.component';
import { DrugCardComponent } from './drug-card/drug-card.component';
import { MedicsComponent } from './medics/medics.component';
import { MedicationsComponent } from './medications/medications.component';
import { MedicationFormComponent } from './medications/medication-form/medication-form.component';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatRadioModule } from '@angular/material/radio';
import { ReactiveFormsModule } from '@angular/forms';
import { MedicFormComponent } from './medics/medic-form/medic-form.component';
import { MedicPageComponent } from './medics/medic-page/medic-page.component';
import { MedicLocationComponent } from './medics/medic-location/medic-location.component';
import { MedicScheduleComponent } from './medics/medic-schedule/medic-schedule.component';
import { MedicProfileComponent } from './medics/medic-profile/medic-profile.component';
import { CalendarModule, DateAdapter } from 'angular-calendar';
import { adapterFactory } from 'angular-calendar/date-adapters/date-fns';

@NgModule({
  declarations: [
    AppComponent,
    NavbarComponent,
    HomeComponent,
    MedicCardComponent,
    DrugCardComponent,
    MedicsComponent,
    MedicationsComponent,
    MedicationFormComponent,
    MedicFormComponent,
    MedicPageComponent,
    MedicLocationComponent,
    MedicScheduleComponent,
    MedicProfileComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    HttpClientModule,
    LayoutModule,
    MatToolbarModule,
    MatButtonModule,
    MatSidenavModule,
    MatIconModule,
    MatListModule,
    MatGridListModule,
    MatCardModule,
    MatInputModule,
    MatSelectModule,
    MatRadioModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatMenuModule,
    MatExpansionModule,
    ReactiveFormsModule,
    BrowserModule,
    AppRoutingModule,
    CalendarModule.forRoot({
      provide: DateAdapter,
      useFactory: adapterFactory,
    }),
  ],
  providers: [],
  bootstrap: [AppComponent],
})
export class AppModule {}
