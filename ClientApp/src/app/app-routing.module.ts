import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MedicsComponent } from './medics/medics.component';
import { MedicationsComponent } from './medications/medications.component';
import { HomeComponent } from './home/home.component';
import { MedicPageComponent } from './medics/medic-page/medic-page.component';

const routes: Routes = [
  { path: 'home', component: HomeComponent },
  {
    path: 'medics',
    component: MedicsComponent,
  },
  {
    path: 'medics/:id',
    component: MedicPageComponent,
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
