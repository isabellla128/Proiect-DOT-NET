import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MedicsComponent } from './medics/medics.component';
import { MedicationsComponent } from './medications/medications.component';
import { HomeComponent } from './home/home.component';
import { MedicPageComponent } from './medics/medic-page/medic-page.component';
import { MedicProfileComponent } from './medics/medic-profile/medic-profile.component';
import { MedicScheduleComponent } from './medics/medic-schedule/medic-schedule.component';
import { MedicLocationComponent } from './medics/medic-location/medic-location.component';

const routes: Routes = [
  { path: 'home', component: HomeComponent },
  {
    path: 'medics',
    component: MedicsComponent,
  },
  {
    path: 'medics/:id',
    component: MedicPageComponent,
    children: [
      {
        path: 'profile',
        component: MedicProfileComponent,
      },
      {
        path: 'schedule',
        component: MedicScheduleComponent,
      },
      {
        path: 'locations',
        component: MedicLocationComponent,
      },
    ],
  },
  { path: 'medications', component: MedicationsComponent },
  { path: '', redirectTo: 'home', pathMatch: 'full' },
  { path: '**', redirectTo: 'home' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
