import { Component, ChangeDetectionStrategy, OnInit } from '@angular/core';
import { isSameDay, isSameMonth } from 'date-fns';
import { Subject } from 'rxjs';
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
        this.events = this.events.filter((iEvent) => iEvent !== event);
        this.handleEvent('Deleted', event);
      },
    },
  ];

  refresh = new Subject<void>();

  events: MyCalendarEvent[] = [];

  activeDayIsOpen: boolean = true;

  constructor(
    private doctorService: DoctorService,
    private patientService: PatientService,
    private appointmentService: AppointmentsService,
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
          this.events.push({
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
    this.events = this.events.map((iEvent) => {
      if (iEvent === event) {
        return {
          ...(event as MyCalendarEvent),
          start: newStart,
          end: newEnd,
        };
      }
      return iEvent;
    });
    this.handleEvent('Dropped or resized', event);
  }

  handleEvent(action: string, event: CalendarEvent): void {}

  addAppointment(): void {
    this.events = [
      ...this.events,
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
    ];
    this.doctorService.postAppointment(this.doctorId, this.appointmentModel);
  }

  deleteEvent(eventToDelete: MyCalendarEvent) {
    this.events = this.events.filter((event) => event !== eventToDelete);
    this.appointmentService.delete(eventToDelete.id || '');
  }

  setView(view: CalendarView) {
    this.view = view;
  }

  closeOpenMonthViewDay() {
    this.activeDayIsOpen = false;
  }
}
