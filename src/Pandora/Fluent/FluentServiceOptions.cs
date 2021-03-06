/*
 * Copyright 2009 Daniel H�lbling - http://www.tigraine.at
 * 
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 * 
 *  http://www.apache.org/licenses/LICENSE-2.0
 *  
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

namespace Pandora.Fluent
{
    using System;

    public class FluentServiceOptions<T>
    {
        private readonly NormalRegistrationCommand registration;

        public FluentServiceOptions(NormalRegistrationCommand registration)
        {
            this.registration = registration;
        }

        public FluentImplementorOptions<T> Implementor<S>() where S : T
        {
            return Implementor(typeof (S));
        }

        public FluentImplementorOptions<T> Implementor(Type implementorType)
        {
            registration.Implementor = implementorType;
            return new FluentImplementorOptions<T>(registration);
        }

        public void Instance<S>(S instance) where S : T
        {
            registration.SetInstance(instance);
        }
    }
}