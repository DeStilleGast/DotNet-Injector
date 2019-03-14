// Copyright © 2006 Microsoft Corporation.  All Rights Reserved

#pragma once

__declspec( dllexport )
int __stdcall MessageHookProc(int nCode, WPARAM wparam, LPARAM lparam);

extern "C" __declspec(dllexport) void test() {
	MessageBox(NULL, TEXT("BEEP BEEP IM A A SHEEP"), TEXT("BEEP BEEP"), MB_OK);
}

namespace ManagedInjector {

	public ref class Injector : System::Object {

	public:
		static void Launch(System::IntPtr, System::Reflection::Assembly^ assemblyName, System::String^ className, System::String^ methodName);
	};
}

/*extern "C" __declspec(dllexport) void InjectTo(HWND windowHandle, System::Reflection::Assembly^ assemblyName, wchar_t * className, wchar_t * methodName) {
	System::String^ const methName = gcnew System::String(methodName);
	System::String^ const clsName = gcnew System::String(className);

	

	ManagedInjector::Injector::Launch(windowHandle, assemblyName, clsName, methName);
}*/
