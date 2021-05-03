import { Injectable } from '@angular/core';
import { DateTime } from 'src/app/interfaces/date-time.interface';

@Injectable()
export class DateHelperService {
  public get dateNow(): Date {
    return new Date();
  }

  public get dateTimeUTCNow(): DateTime {
    return this.iso8601ToDateTimeUTC(this.dateNow.toISOString());
  }

  public dateTimeToISO8601UTC(dateTime: DateTime): string {
    const {
      date: { day, month, year },
      time: { second, minute, hour },
    } = dateTime;

    function to2Signed(value: number): string | number {
      return value < 10 ? `0${value}` : value;
    }

    return `${year}-${to2Signed(month)}-${to2Signed(day)}T${to2Signed(
      hour
    )}:${to2Signed(minute)}:${to2Signed(second)}Z`;
  }

  public dateTimeComparer(dateTime1: DateTime, dateTime2: DateTime): number {
    return (
      Date.parse(this.dateTimeToISO8601UTC(dateTime1)) -
      Date.parse(this.dateTimeToISO8601UTC(dateTime2))
    );
  }

  public iso8601ToDateTimeUTC(isoDate: string): DateTime {
    const date = new Date(Date.parse(isoDate));
    return {
      date: {
        year: date.getUTCFullYear(),
        month: date.getUTCMonth() + 1,
        day: date.getUTCDate(),
      },
      time: {
        hour: date.getUTCHours(),
        minute: date.getUTCMinutes(),
        second: date.getUTCSeconds(),
      },
    };
  }
}
