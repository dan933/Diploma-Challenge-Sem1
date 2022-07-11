import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CustomersTableComponent } from './Components/customers-table/customers-table.component';
import { OrderComponent } from './Components/order/order.component';
import { OrderFormComponent } from './Components/orders/order-form/order-form.component';
import { OrdersComponent } from './Components/orders/orders.component';

const routes: Routes = [
  { path: 'customers', component: CustomersTableComponent },
  { path: 'customer/:id', component: OrdersComponent },
  { path: 'order-form/:id', component: OrderFormComponent },
  { path: 'order/:id', component: OrderComponent },
  { path: '**', redirectTo:'customers' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
