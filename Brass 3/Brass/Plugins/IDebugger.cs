namespace BeeDevelopment.Brass3.Plugins {
	
	/// <summary>
	/// Defines the interface that debuggers need to implement.
	/// </summary>
	public interface IDebugger : IPlugin {

		/// <summary>
		/// Starts the debugger.
		/// </summary>
		/// <param name="compiler">The compiler instance to start the debugger with.</param>
		/// <param name="debuggingEnabled">True if the debugger is enabled by default, false if we wish to run without debugging.</param>
		void Start(Compiler compiler, bool debuggingEnabled);

	}
}