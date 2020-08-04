export class DashboardService {
	public static async getDashboardUrlAsync(): Promise<string> {
		const response = await fetch("/api/powerBiDashboard");
		return response.text();
	}
}