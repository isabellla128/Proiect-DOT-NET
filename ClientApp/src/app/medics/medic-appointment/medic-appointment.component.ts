import { Component, ChangeDetectionStrategy, OnInit } from '@angular/core';
import { isSameDay, isSameMonth } from 'date-fns';
import { Observable, Subject, of, BehaviorSubject } from 'rxjs';
import {
  CalendarEvent,
  CalendarEventAction,
  CalendarEventTimesChangedEvent,
  CalendarView,
} from 'angular-calendar';
import { EventColor } from 'calendar-utils';
import { DoctorService } from 'src/shared/services/doctor.service';
import { ActivatedRoute } from '@angular/router';
import { PatientService } from 'src/shared/services/patient.service';
import { Appointment, MyCalendarEvent } from 'src/models/appointment';
import { AppointmentsService } from 'src/shared/services/appointments.service';
import { MatSnackBar } from '@angular/material/snack-bar';

const colors: Record<string, EventColor> = {
  yellow: {
    primary: '#ffd740',
    secondary: '#FDF1BA',
  },
  purple: {
    primary: '#673ab7',
    secondary: '#FAE3E3',
  },
};

@Component({
  selector: 'app-medic-appointment',
  changeDetection: ChangeDetectionStrategy.OnPush,
  styleUrls: ['./medic-appointment.component.css'],
  templateUrl: './medic-appointment.component.html',
})
export class MedicAppointmentComponent implements OnInit {
  doctorId: string = '';

  view: CalendarView = CalendarView.Month;

  CalendarView = CalendarView;

  viewDate: Date = new Date();
  currentDate: Date = new Date();

  appointmentModel: Appointment = {
    id: '',
    doctorId: '',
    patientId: '',
    startTime: '',
    endTime: '',
  };

  modalData:
    | {
        action: string;
        event: CalendarEvent;
      }
    | undefined;

  actions: CalendarEventAction[] = [
    {
      label: '<i class="fas fa-fw fa-pencil-alt"></i>',
      a11yLabel: 'Edit',
      onClick: ({ event }: { event: CalendarEvent }): void => {
        this.handleEvent('Edited', event);
      },
    },
    {
      label: '<i class="fas fa-fw fa-trash-alt"></i>',
      a11yLabel: 'Delete',
      onClick: ({ event }: { event: CalendarEvent }): void => {
        this.events$.next(
          this.events$.getValue().filter((iEvent) => iEvent !== event)
        );
        this.handleEvent('Deleted', event);
      },
    },
  ];

  refresh = new Subject<void>();

  defaultEvents: MyCalendarEvent[] = [];

  events$: BehaviorSubject<MyCalendarEvent[]> = new BehaviorSubject<
    MyCalendarEvent[]
  >([]);

  activeDayIsOpen: boolean = true;

  constructor(
    private doctorService: DoctorService,
    private patientService: PatientService,
    private appointmentService: AppointmentsService,
    private snackBar: MatSnackBar,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.appointmentModel.patientId = this.patientService.patientId;
    this.route.parent?.params.subscribe((params) => {
      (this.doctorId = params['id']),
        (this.appointmentModel.doctorId = params['id']);
    });
    this.doctorService.getAppointments(this.doctorId).subscribe({
      next: (appointments) => {
        console.log(appointments);

        appointments.forEach((appointment, index) => {
          const newEvents = this.events$.getValue();
          newEvents.push({
            id: appointment.id,
            start: new Date(appointment.startTime),
            end: new Date(appointment.endTime),
            title: 'Appointment' + index,
            color: { ...colors['purple'] },
            actions: this.actions,
            resizable: {
              beforeStart: true,
              afterEnd: true,
            },
            draggable: true,
          });
          this.events$.next(newEvents);
        });
      },
      error: (error) => console.log(error),
    });
  }

  dayClicked({ date, events }: { date: Date; events: CalendarEvent[] }): void {
    if (isSameMonth(date, this.viewDate)) {
      if (
        (isSameDay(this.viewDate, date) && this.activeDayIsOpen === true) ||
        events.length === 0
      ) {
        this.activeDayIsOpen = false;
      } else {
        this.activeDayIsOpen = true;
      }
      this.viewDate = date;
    }
  }

  eventTimesChanged({
    event,
    newStart,
    newEnd,
  }: CalendarEventTimesChangedEvent): void {
    this.events$.next(
      this.events$.getValue().map((iEvent) => {
        if (iEvent === event) {
          return {
            ...(event as MyCalendarEvent),
            start: newStart,
            end: newEnd,
          };
        }
        return iEvent;
      })
    );
    this.handleEvent('Dropped or resized', event);
  }

  handleEvent(action: string, event: CalendarEvent): void {}

  addAppointment(): void {
    this.doctorService
      .postAppointment(this.doctorId, this.appointmentModel)
      .subscribe({
        next: (result) => {
          this.events$.next([
            ...this.events$.getValue(),
            {
              title: 'Appointment',
              start: new Date(this.appointmentModel.startTime),
              end: new Date(this.appointmentModel.endTime),
              color: colors['purple'],
              draggable: true,
              resizable: {
                beforeStart: true,
                afterEnd: true,
              },
            },
          ]);
        },
        error: (error) => {
          this.snackBar.open('There is an error!', 'Confirm', {
            horizontalPosition: 'center',
            verticalPosition: 'bottom',
          });
        },
        complete: () => {},
      });
  }

  deleteEvent(eventToDelete: MyCalendarEvent) {
    this.events$.next(
      this.events$.getValue().filter((event) => event !== eventToDelete)
    );
    this.appointmentService.delete(eventToDelete.id || '');
  }

  setView(view: CalendarView) {
    this.view = view;
  }

  closeOpenMonthViewDay() {
    this.activeDayIsOpen = false;
  }
}
