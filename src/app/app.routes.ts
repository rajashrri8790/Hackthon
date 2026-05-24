import { Routes } from '@angular/router';

import { Home } from './pages/home/home';
import { Search } from './pages/search/search';
import { Rooms } from './pages/rooms/rooms';
import { RoomDetails } from './pages/room-details/room-details';
import { Booking } from './pages/booking/booking';
import { Payment } from './pages/payment/payment';
import { Success } from './pages/success/success';
import { SigninComponent } from './pages/signin/signin';

export const routes: Routes = [
  { path: '', redirectTo: 'signin', pathMatch: 'full' },

  { path: 'signin', component: SigninComponent },

  { path: 'home', component: Home },
  { path: 'search', component: Search },
  { path: 'rooms', component: Rooms },
  { path: 'room-details', component: RoomDetails },
  { path: 'booking', component: Booking },
  { path: 'payment', component: Payment },
  { path: 'success', component: Success }
];