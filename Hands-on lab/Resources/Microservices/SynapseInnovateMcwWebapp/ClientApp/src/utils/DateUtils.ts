export class DateUtils {
	public static formatDate(date: Date | null): string {
		if (!date) return "";

		let d = new Date(date),
			month = '' + (d.getUTCMonth() + 1),
			day = '' + d.getUTCDate(),
			year = d.getUTCFullYear();

		if (month.length < 2)
			month = '0' + month;
		if (day.length < 2)
			day = '0' + day;

		return [year, month, day].join('-');
	}
	
	public static formatTime(date: Date | null): string {
		if (!date) return "";
		
		let d = new Date(date),
			hour = '' + d.getHours(),
			minutes = '' + d.getMinutes(),
			seconds = '' + d.getSeconds();
		
		if (hour.length < 2)
			hour = '0' + hour;
		if (minutes.length < 2)
			minutes = '0' + minutes;
		if (seconds.length < 2)
			seconds = '0' + seconds;
		
		return [hour, minutes, seconds].join(":");
	}
}